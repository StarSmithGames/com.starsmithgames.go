using MoreMountains.NiceVibrations;

namespace StarSmithGames.Go.VibrationService
{
    public interface IVibrationService
    {
		void Vibrate();
		void Vibrate(HapticTypes haptic);
	}

    public class VibrationService : IVibrationService
    {
		public void Vibrate()
		{
			Vibrate(HapticTypes.HeavyImpact);
		}

		public void Vibrate(HapticTypes haptic)
		{
			MMVibrationManager.Haptic(haptic);
		}
	}
}