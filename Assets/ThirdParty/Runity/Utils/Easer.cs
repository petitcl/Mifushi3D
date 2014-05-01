using UnityEngine;
using System.Collections;

namespace Runity {

	namespace Utils {

		public	delegate float		EaseFunc(float t);

		public	enum EaseType {
			Linear,
			QuadIn,
			QuadOut
		}

		public	static	class Ease {
		}

	}

}
