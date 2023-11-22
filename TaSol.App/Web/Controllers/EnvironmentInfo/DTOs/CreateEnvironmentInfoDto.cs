namespace Web.Controllers.EnvironmentInfo.DTOs;

public class CreateEnvironmentInfoDto
{
    public int Humidity { get; set; }

    public double Temperature { get; set; }

    public int LightLevel { get; set; }

    public int RainLevel { get; set; }
}
