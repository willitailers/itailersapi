namespace API_Itailers_Core.Models
{
    public class VendorThemeRequest
    {
        public int id_cliente { get; set; }
        public string vendorDomainName { get; set; }
        public string primaryColor { get; set; }
        public string secondaryColor { get; set; }
        public string logoImage { get; set; }
        public string vendorTitleImage { get; set; }
        public string bannerImage { get; set; }
        public bool isDarkTheme { get; set; }
        public string bannerImageMobile { get; set; }
    }
}
