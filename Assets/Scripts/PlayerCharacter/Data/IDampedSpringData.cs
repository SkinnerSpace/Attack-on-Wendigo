public interface IDampedSpringData
{
    float Power { get; }
    float Time { get; }
    float Amplitude { get; set; }
    float CurrentTime { get; set; }
}
