using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO : Change List<MaterialAnimation> to HashTable for optimization
//TODO : Change MaterialAnimation.TargetMaterial to Array<Material> for scalability
namespace Runity {
	public class MaterialAnimator : MonoBehaviour {
		
		//public types
		[System.Serializable]
		public class MaterialAnimation {
			//public attributes
			public	string		Name;
			public	Material	TargetMaterial;
			public	float		FadeTime;
			public	bool		Enabled = false;
			
			//private attributes
			private	float		startTime = 0.0f;
			private	Color		startColor;
			private	Color		endColor;
			

			public	void		FadeTo(Color newColor) {
				if (this.TargetMaterial.color.Equals(newColor)) return;
				this.startColor = this.TargetMaterial.color;
				this.endColor = newColor;
				this.startTime = Time.time;
				this.Enabled = true;
			}

			public	void		Update() {
				if (!this.Enabled) return;

				float currTime = Time.time - this.startTime;
				float offset = currTime / this.FadeTime;
				Color nColor = Color.Lerp(this.startColor, this.endColor, offset);
				this.TargetMaterial.color = nColor;
				if (nColor.Equals(this.endColor)) {
					this.Enabled = false;
				}
			}
		}
		
		
		//public attributes
		public	List<MaterialAnimation>	Materials = new List<MaterialAnimation>();

		//public methods

		public void		FadeTo(string label, Color newColor) {
			foreach (MaterialAnimation matAnim in this.Materials) {
				if (!matAnim.Name.Equals(label)) continue;
				matAnim.FadeTo(newColor);
				return;
			}
		}
		
		//private Unity callbacks
		private	void	Update() {
			foreach (MaterialAnimation matAnim in this.Materials) {
				if (!matAnim.Enabled) continue;
				matAnim.Update();
			}
		}
	}
}
