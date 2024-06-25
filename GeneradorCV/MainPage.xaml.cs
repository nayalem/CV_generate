using System;
using Xamarin.Forms;

namespace GeneradorCV
{
    public partial class MainPage : ContentPage
    {
        private readonly TextFileService _fileService;

        public MainPage()
        {
            InitializeComponent();

            // Inicializar el servicio de archivo de texto con un nombre de archivo específico
            _fileService = new TextFileService("cv_data.txt");

            // Cargar los datos previamente guardados al inicializar la página
            CargarDatosGuardados();
        }

        private async void OnEnviarCVClicked(object sender, EventArgs e)
        {
            var _nombre = NombreEntry.Text;
            var _cargo = CargoEntry.Text;
            var _correo = CorreoEntry.Text;
            var _telefono = TelefonoEntry.Text;
            var _empresa = EmpresaEntry.Text;
            var _experiencia = ExperienciaEditor.Text;
            var _educacion = EducacionEditor.Text;
            var _habilidades = HabilidadesEditor.Text;

            if (string.IsNullOrWhiteSpace(_nombre) || string.IsNullOrWhiteSpace(_correo))
            {
                await DisplayAlert("Error", "Por favor complete todos los campos obligatorios.", "OK");
                return;
            }

            // Generar el texto del CV
            string cvText = GenerarTextoCV(_nombre, _cargo, _correo, _telefono, _empresa, _experiencia, _educacion, _habilidades);

            // Guardar el CV en el archivo
            await _fileService.WriteTextAsync(cvText);

            // Mostrar mensaje de éxito
            await DisplayAlert("Correcto", "CV enviado correctamente", "OK");

            // Mostrar el CV generado con los datos ingresados
            MostrarCVGenerado(_nombre, _cargo, _correo, _telefono, _empresa, _experiencia, _educacion, _habilidades);
        }

        private async void CargarDatosGuardados()
        {
            // Leer el texto guardado del archivo y mostrar los datos si hay alguno
            string savedText = await _fileService.ReadTextAsync();
            if (!string.IsNullOrWhiteSpace(savedText))
            {
                string[] cvData = savedText.Split('|'); // Asumiendo que el formato es separado por '|'

                // Mostrar los datos guardados en los controles correspondientes
                NombreEntry.Text = cvData[0];
                CargoEntry.Text = cvData[1];
                CorreoEntry.Text = cvData[2];
                TelefonoEntry.Text = cvData[3];
                EmpresaEntry.Text = cvData[4];
                ExperienciaEditor.Text = cvData[5];
                EducacionEditor.Text = cvData[6];
                HabilidadesEditor.Text = cvData[7];

                // Mostrar el CV generado con los datos guardados
                MostrarCVGenerado(cvData[0], cvData[1], cvData[2], cvData[3], cvData[4], cvData[5], cvData[6], cvData[7]);
            }
        }

        private void MostrarCVGenerado(string nombre, string cargo, string correo, string telefono, string empresa, string experiencia, string educacion, string habilidades)
        {
            NombreLabel.Text = $"Nombre: {nombre}";
            CargoLabel.Text = $"Cargo: {cargo}";
            CorreoLabel.Text = $"Correo: {correo}";
            TelefonoLabel.Text = $"Teléfono: {telefono}";
            EmpresaLabel.Text = $"Empresa: {empresa}";
            ExperienciaLabel.Text = $"Experiencia Laboral:\n{experiencia}";
            EducacionLabel.Text = $"Educación:\n{educacion}";
            HabilidadesLabel.Text = $"Habilidades:\n{habilidades}";

            // Mostrar el contenedor del CV generado
            CVGeneradoFrame.IsVisible = true;
        }

        private string GenerarTextoCV(string nombre, string cargo, string correo, string telefono, string empresa, string experiencia, string educacion, string habilidades)
        {
            // Generar el texto del CV con un formato específico
            return $"{nombre}|{cargo}|{correo}|{telefono}|{empresa}|{experiencia}|{educacion}|{habilidades}";
        }

        private void LimpiarCampos_Clicked(object sender, EventArgs e)
        {
            // Lógica para limpiar los campos del formulario
            NombreEntry.Text = string.Empty;
            CargoEntry.Text = string.Empty;
            CorreoEntry.Text = string.Empty;
            TelefonoEntry.Text = string.Empty;
            EmpresaEntry.Text = string.Empty;
            ExperienciaEditor.Text = string.Empty;
            EducacionEditor.Text = string.Empty;
            HabilidadesEditor.Text = string.Empty;

            // Ocultar el contenedor de CV generado
            CVGeneradoFrame.IsVisible = false;
        }
    }
}



















