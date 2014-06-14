﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameAnimator : Runity.MonoBehaviourExt {

	//public types
	//TODO : maybe refactor this into IEnumerator AnimationMethod(SimpleCallback cb)
	public	delegate	void	AnimationMethod(EndTransitionCallback cb);

	//static attributes
	private	static	GameAnimator	_instance = null;
	public	static	GameAnimator	Instance {
		get {
			return GameAnimator._instance;
		}
	}

	//public attributes
	public	Runity.FadeInOut			FadeInOut;
	public	Runity.MaterialAnimator		MatAnimator;

	public	Material	Red;
	public	Material	Green;
	public	Material	Blue;
	public	Material	White;
	public	Shader		SolidColorShader;
	public	Shader		FadedColorShader;



	//private attributes
	private	Dictionary<string, AnimationMethod>	animations = new Dictionary<string, AnimationMethod>();

	//public methods
	public	Material	GameColorToMaterial(GameLevel.GameColor color) {
		switch (color) {
		case GameLevel.GameColor.Red:
			return this.Red;
		case GameLevel.GameColor.Green:
			return this.Green;
		case GameLevel.GameColor.Blue:
			return this.Blue;
		default:
			return this.White;
		}
	}

	public	void	PlayAnimation(string animationName) {
		if (!this.animations.ContainsKey(animationName)) return;
		AnimationMethod method = this.animations[animationName];
		method(this.emptyCallback);
	}

	public	void	PlayAnimation(string animationName, EndTransitionCallback callback) {
		if (!this.animations.ContainsKey(animationName)) {
			callback();
			return;
		}
		AnimationMethod method = this.animations[animationName];
		method(callback);
	}

	//private Unity callbacks
	private	void	Awake() {
		GameAnimator._instance = this;
		//handcoded animation initialization
		this.animations["Game.Start"] = this.gameStartAnimation;
	}

	private	void	Start() {
		Runity.Messenger<GameLevel.GameColor>.AddListener("Player.ChangeColor", this.onPlayerChangeColor);
	}

	//private animation methods
	private	void	gameStartAnimation(EndTransitionCallback callback) {
		float fadeTime = this.FadeInOut.fadeTime;
		this.FadeInOut.gameObject.SetActive(true);
		this.FadeInOut.FadeIn();
		this.WaitForNSeconds(fadeTime, callback);
	}

	//private Messenger callbacks
	private	void	onPlayerChangeColor(GameLevel.GameColor newColor) {
		switch (newColor) {
		case GameLevel.GameColor.Blue:
			this.MatAnimator.FadeTo("Player", GameLevel.Instance.Blue);

			this.Red.shader = this.FadedColorShader;
			this.Green.shader = this.FadedColorShader;


			this.MatAnimator.FadeTo("Red", GameLevel.Instance.FadedRed);
			this.MatAnimator.FadeTo("Green", GameLevel.Instance.FadedGreen);
			this.MatAnimator.FadeTo("Blue", GameLevel.Instance.Blue, this.TransitionToSolidEnded);
			break;
		case GameLevel.GameColor.Red:
			this.MatAnimator.FadeTo("Player", GameLevel.Instance.Red);

			this.Green.shader = this.FadedColorShader;
			this.Blue.shader = this.FadedColorShader;
			

			this.MatAnimator.FadeTo("Red", GameLevel.Instance.Red, this.TransitionToSolidEnded);
			this.MatAnimator.FadeTo("Green", GameLevel.Instance.FadedGreen);
			this.MatAnimator.FadeTo("Blue", GameLevel.Instance.FadedBlue);
			break;
		case GameLevel.GameColor.Green:
			this.MatAnimator.FadeTo("Player", GameLevel.Instance.Green);

			this.Red.shader = this.FadedColorShader;
			this.Blue.shader = this.FadedColorShader;


			this.MatAnimator.FadeTo("Red", GameLevel.Instance.FadedRed);
			this.MatAnimator.FadeTo("Green", GameLevel.Instance.Green, this.TransitionToSolidEnded);
			this.MatAnimator.FadeTo("Blue", GameLevel.Instance.FadedBlue);
			break;
		default:
			break;
		}
	}

	private	void	TransitionToSolidEnded(Material mat) {
		mat.shader = this.SolidColorShader;
	}

	private void emptyCallback() {

	}
}
