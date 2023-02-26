public class AmmoReporter
{
    private FunctionTimer timer;
    private WeaponSFXPlayer sfxPlayer;

    private bool ableToReport = true;

    public AmmoReporter(FunctionTimer timer, WeaponSFXPlayer sfxPlayer)
    {
        this.timer = timer;
        this.sfxPlayer = sfxPlayer;
    }

    public void ReportIsEmpty()
    {
        if (ableToReport)
        {
            ableToReport = false;
            AmmoBar.Instance.UpdateAmmo(0);
            sfxPlayer.PlayIsEmptySFX();

            timer.Set("Enable Report", 0.5f, EnableReport);
        }
    }

    private void EnableReport() => ableToReport = true;
}
