using Godot;
using System;
using System.Runtime.Intrinsics.X86;

public partial class InteractionBox : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void _on_area_entered(Area2D body) {
		GD.Print(body.Name);
		if (body is IInteractable) {
			GD.Print("fuck");
			IInteractable b2 = body as IInteractable;
			b2.Interact();
		}
	}

}

