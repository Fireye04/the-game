using Godot;
using System;
using System.Collections;
using System.Runtime.Intrinsics.X86;

public partial class InteractionBox : Area2D
{

	[Signal]
	public delegate void IsInteractableEventHandler();

	public ArrayList objectsInRange = new ArrayList();
	public Node2D playerRef;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	

		// for (int i = 0; i < objectsInRange.Count; i++) {
		// 	GD.Print(objectsInRange[i]);
		// }
	}

	public void interact() {
		// Should never be called when objectsInRange is empty but i put in a try just in case
		try {
			IInteractable item = objectsInRange[0] as IInteractable;
			item.Interact();
		} catch (Exception e) {
			GD.Print(e);
		}
	}
	private void _on_area_entered(Area2D area) {
		if (area is IInteractable) {
			objectsInRange.Add(area as IInteractable);
			EmitSignal(SignalName.IsInteractable);
		}
	}

	private void _on_area_exited(Area2D area) {
		if (area is IInteractable) {
			for (int i = 0; i < objectsInRange.Count; i++) {
				if (objectsInRange[i] == area){
					objectsInRange.Remove(area as IInteractable);
					if (objectsInRange.Count <= 0) {
						EmitSignal(SignalName.IsInteractable);
					}
				}
			}
		}
	}


}



