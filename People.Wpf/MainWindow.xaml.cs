using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using People.Wpf.Models;

namespace People.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _http = new HttpClient();
        private readonly ObservableCollection<Person> _people = new ObservableCollection<Person>();
        private Person? _selected;

        public MainWindow()
        {
            InitializeComponent();

            // API da nuvem
            ApiUrlText.Text = "https://peoplesolution-cve3e3hwc6dfbnbx.brazilsouth-01.azurewebsites.net";

            PeopleGrid.ItemsSource = _people;
            Loaded += async (_, __) => await LoadPeopleAsync();
        }

        private string BaseUrl => ApiUrlText.Text?.Trim().TrimEnd('/');

        public JsonSerializerOptions? JsonOpts { get; private set; }

        private async Task LoadPeopleAsync()
        {
            try
            {
                _people.Clear();
                var people = await _http.GetFromJsonAsync<Person[]>($"{BaseUrl}/api/people");
                if (people != null)
                {
                    foreach (var p in people) _people.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PeopleGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selected = PeopleGrid.SelectedItem as Person;
            if (_selected == null)
            {
                ClearForm();
                return;
            }

            NomeBox.Text = _selected.Nome;
            SobrenomeBox.Text = _selected.Sobrenome;
            TelefoneBox.Text = _selected.Telefone;
        }

        private void ClearForm()
        {
            NomeBox.Text = "";
            SobrenomeBox.Text = "";
            TelefoneBox.Text = "";
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadPeopleAsync();
        }

        private async void Novo_Click(object sender, RoutedEventArgs e)
        {
            PeopleGrid.UnselectAll();
            _selected = null;
            ClearForm();
            NomeBox.Focus();
        }

        private bool ValidateForm(out Person dto)
        {
            dto = new Person
            {
                Nome = NomeBox.Text?.Trim(),
                Sobrenome = SobrenomeBox.Text?.Trim(),
                Telefone = TelefoneBox.Text?.Trim()
            };

            if (string.IsNullOrWhiteSpace(dto.Nome))
            {
                MessageBox.Show("Nome é obrigatório.", "Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private async void Salvar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm(out var dto)) return;

            try
            {
                if (_selected == null)
                {
                    // CREATE (POST), não envia Id
                    var payload = new { dto.Nome, dto.Sobrenome, dto.Telefone };

                    var url = $"{BaseUrl}/api/people";
                    var response = await _http.PostAsJsonAsync(url, payload, JsonOpts);

                    if (!response.IsSuccessStatusCode)
                    {
                        var err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"POST falhou: {(int)response.StatusCode} {response.ReasonPhrase}\n{err}",
                            "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    // UPDATE (PUT), vai com Id
                    dto.Id = _selected.Id;
                    var response = await _http.PutAsJsonAsync($"{BaseUrl}/api/people/{_selected.Id}", dto, JsonOpts);
                    response.EnsureSuccessStatusCode();
                }

                await LoadPeopleAsync();
                PeopleGrid.UnselectAll();
                _selected = null;
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Excluir_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null)
            {
                MessageBox.Show("Selecione um registro na tabela para excluir.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (MessageBox.Show("Confirma a exclusão?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            try
            {
                var response = await _http.DeleteAsync($"{BaseUrl}/api/people/{_selected.Id}");
                response.EnsureSuccessStatusCode();

                await LoadPeopleAsync();
                PeopleGrid.UnselectAll();
                _selected = null;
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
