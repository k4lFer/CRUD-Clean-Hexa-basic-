using Shared.Settings.Objects;

namespace Shared.Settings
{
    public class GlobalSettings
    {
        public DatabaseSettings dbSettings{ get; set; } = new ();
        public JwtSettings jwtSettings{ get; set; } = new ();
        public CloudinarySettings cloudinarySettings{ get; set; } = new ();
        public CorsSettings corsSettings{ get; set; } = new ();
    }
}