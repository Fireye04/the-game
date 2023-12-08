using Godot;
using System;

public partial class Player : Node2D
{
	
	[Export]
	public int Speed { get; set; } = 400; // How fast the player will move (pixels/sec).

	[Export]
	public RichTextLabel interactPrompt;

	private bool interactionOn = false;

	public Vector2 ScreenSize; // Size of the game window.

	private AnimatedSprite2D animatedSprite2D;

	private InteractionBox iBox;
	
	// Tracks last moved direction for idle 
	// could be an enum but i'm a lazy bitch
	private string dir = "p_i_down";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		ScreenSize = GetViewportRect().Size;
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		iBox = GetNode("InteractionBox") as InteractionBox;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		// currently just movement for out of combat, 
		// will have to be toggled when turn based is triggered
		// TODO: Toggle turn based

		//tutorial: https://docs.godotengine.org/en/stable/getting_started/first_2d_game/02.player_scene.html

		//process inputs
		var velocity = Vector2.Zero; 

		if (Input.IsActionPressed("move_right")) {
			velocity.X += 1;
		}

		if (Input.IsActionPressed("move_left")) {
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("move_down")) {
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("move_up")) {
			velocity.Y -= 1;
		}

		if (velocity.Length() > 0) {
			velocity = velocity.Normalized() * Speed;
		}
			
		animatedSprite2D.Play();

		
		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		

		if (velocity.X != 0) {
			if (velocity.X > 0) {
				animatedSprite2D.Animation = "p_w_right";
				dir = "p_i_right";
			} else if (velocity.X < 0) {
				animatedSprite2D.Animation = "p_w_left";
				dir = "p_i_left";
			}
			
		} else if (velocity.Y != 0) {
			if (velocity.Y < 0) {
				animatedSprite2D.Animation = "p_w_up";
				dir = "p_i_up";
			} else if (velocity.Y > 0) {
				animatedSprite2D.Animation = "p_w_down";
				dir = "p_i_down";
			}

		} else {
			animatedSprite2D.Animation = dir;
		}
		
		if ((velocity.X != 0 || velocity.Y != 0) && interactionOn) {
			IInteractable iItem = iBox.getFocused(GlobalPosition);
			if (iItem != null) {
				interactPrompt.Text = "Press F to interact with " + iItem.getItemName();
			}
			
		}
	}

	public override void _Input(InputEvent @event)
{	
	if (interactionOn) {
		if (@event.IsActionPressed("interact")) {
			iBox.interact(GlobalPosition);
		}
	}
}


	private void _on_interaction_box_is_interactable()
	{

		// I apologize for my crimes against humanity

		switch (interactionOn){
			case true:
				interactionOn = false;
				interactPrompt.Hide();
				break;
			case false:
				interactionOn = true;
				interactPrompt.Show();
				break;
		}
	}
}



