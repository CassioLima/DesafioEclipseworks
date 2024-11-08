using System.Security.Policy;

namespace Application
{
    public class SettingsDto
    {
        public CRIPTO CRIPTO { get; set; }
    }

    public class CRIPTO
    {
        public string KEY { get; set; }
        public string SECRET { get; set; }
    }
}
