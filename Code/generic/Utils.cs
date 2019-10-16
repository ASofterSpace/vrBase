/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

public class Utils {

	/**
	 * Clamp a value between -180 and 180, such that it is compatible
	 * e.g. with the natural range of euler angles (going from -pi to
	 * pi, showing why 2*pi is cooler than tau! :D)
	 */
	public static float clampRot(float val) {

		while (val < -180) {
			val += 360;
		}

		while (val > 180) {
			val -= 360;
		}

		return val;
	}
}
