namespace KaupunkipyoraAPI.Services.Settings
{
    public class APIOptions
    {
        public ConnectionStringsOptions ConnectionStrings { get; set; } = default!;
        public JWTOptions JWT { get; set; }
    }
}
