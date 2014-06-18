using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO : Change List<MaterialAnimation> to HashTable for optimization
//TODO : Change MaterialAnimation.TargetMaterial to Array<Material> for scalability
//TODO : add method AddMaterial (string, Material, float)
namespace Runity {
	public class MaterialAnimator : MonoBehaviourExt {

		public	delegate void	EndTransitionCallback(Material mat);

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
			private	EndTransitionCallback	callback = null;
			

			public	void		FadeTo(Color newColor) {
				this.callback = null;
				if (this.TargetMaterial.color.Equals(newColor)) return;
				this.startColor = this.TargetMaterial.color;
				this.endColor = newColor;
				this.startTime = Time.time;
				this.Enabled = true;
			}

			public	void		FadeTo(Color newColor, EndTransitionCallback cb) {
				this.FadeTo(newColor);
				this.callback = cb;
			}

			public	void		Update() {
				if (!this.Enabled) return;

				float currTime = Time.time - this.startTime;
				float offset = currTime / this.FadeTime;
				Color nColor = Color.Lerp(this.startColor, this.endColor, offset);
				this.TargetMaterial.color = nColor;
				if (nColor.Equals(this.endColor)) {
					this.Enabled = false;
					if (this.callback != null) this.callback(this.TargetMaterial);
					this.callback = null;
				}
			}
		}
		
		
		//public attributes
		public	List<MaterialAnimation>	Materials = new List<MaterialAnimation>();

		//private attributes
		private	List<MaterialAnimation>	TemporaryMaterials = new List<MaterialAnimation>();

		//public methods

		public	MaterialAnimation	FindAnimationByName(string	name) {
			return this.Materials.Find(anim => anim.Name == name);
		}

		public void		FadeTo(string label, Color newColor) {
			foreach (MaterialAnimation matAnim in this.Materials) {
				if (!matAnim.Name.Equals(label)) continue;
				matAnim.FadeTo(newColor);
				return;
			}
		}

		public void		FadeTo(string label, Color newColor, EndTransitionCallback cb) {
			foreach (MaterialAnimation matAnim in this.Materials) {
				if (!matAnim.Name.Equals(label)) continue;
				matAnim.FadeTo(newColor, cb);
				return;
			}
		}

		public	void	TempFade(Material mat, Color newColor, float time) {
			MaterialAnimation newMatAnim = new MaterialAnimation();
			newMatAnim.Enabled = true;
			newMatAnim.TargetMaterial = mat;
			newMatAnim.FadeTime = time;
			newMatAnim.Name = "tmp";
			newMatAnim.FadeTo(newColor);
			this.TemporaryMaterials.Add(newMatAnim);
		}
		
		public	void	TempFade(Material mat, Color newColor, float time, EndTransitionCallback cb) {
			MaterialAnimation newMatAnim = new MaterialAnimation();
			newMatAnim.Enabled = true;
			newMatAnim.TargetMaterial = mat;
			newMatAnim.FadeTime = time;
			newMatAnim.Name = "tmp";
			newMatAnim.FadeTo(newColor, cb);
			this.TemporaryMaterials.Add(newMatAnim);
		}

		//private Unity callbacks
		private	void	Update() {
			foreach (MaterialAnimation matAnim in this.Materials) {
				if (!matAnim.Enabled) continue;
				matAnim.Update();
			}
			foreach (MaterialAnimation matAnim in this.TemporaryMaterials) {
				if (!matAnim.Enabled) continue;
				matAnim.Update();
			}
			this.TemporaryMaterials.RemoveAll(matAnim => !matAnim.Enabled);
		}
	}
}
