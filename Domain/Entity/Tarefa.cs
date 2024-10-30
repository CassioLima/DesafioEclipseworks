using Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Domain.Entity
{
    public class Tarefa : EntityBase, INotifyPropertyChanged
    {
        public Tarefa() { }
        public Tarefa(Prioridade prioridade)
        {
            this.Prioridade = prioridade;
        }

        private int _projetoId;
        [ForeignKey("Projeto")]
        public int ProjetoId
        {
            get => _projetoId;
            set => SetField(ref _projetoId, value);
        }

        private int _usuarioId;
        [ForeignKey("Usuario")]
        public int UsuarioId
        {
            get => _usuarioId;
            set => SetField(ref _usuarioId, value);
        }

        private string _titulo = string.Empty;
        public string Titulo
        {
            get => _titulo;
            set => SetField(ref _titulo, value);
        }

        private string _descricao = string.Empty;
        public string Descricao
        {
            get => _descricao;
            set => SetField(ref _descricao, value);
        }

        private DateTime _dataVencimento;
        public DateTime DataVencimento
        {
            get => _dataVencimento;
            set => SetField(ref _dataVencimento, value);
        }

        private Status _status;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }

        private Prioridade _prioridade;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Prioridade Prioridade
        {
            get => _prioridade;
            private set => SetField(ref _prioridade, value);
        }

        private IEnumerable<TarefaComentario>? _comentarios;
        public IEnumerable<TarefaComentario>? Comentarios
        {
            get => _comentarios;
            set => SetField(ref _comentarios, value);
        }

        // Lista para armazenar as propriedades modificadas
        private readonly HashSet<string> _propriedadesModificadas = new HashSet<string>();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName != null)
            {
                _propriedadesModificadas.Add(propertyName);
            }
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // Método para recuperar as propriedades modificadas
        public Dictionary<string, object?> ObterPropriedadesModificadas()
        {
            var propriedadesModificadas = new Dictionary<string, object?>();

            foreach (var prop in GetType().GetProperties())
            {
                if (_propriedadesModificadas.Contains(prop.Name))
                {
                    propriedadesModificadas[prop.Name] = prop.GetValue(this);
                }
            }

            return propriedadesModificadas;
        }
    }
}
