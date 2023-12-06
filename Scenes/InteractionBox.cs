using Godot;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;

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

	public void interact(Vector2 pos) {
		// Should never be called when objectsInRange is empty but i put in a try just in case
		try {
			IInteractable item = getFocused(pos) as IInteractable;
			item.Interact();
		} catch (Exception e) {
			GD.Print(e);
		}
	}
	
	public IInteractable getFocused (Vector2 pos){

		if (objectsInRange.Count == 0) {
			return null;
		}

		Area2D minItem = objectsInRange[0] as Area2D;
		float minDis = float.MaxValue;
		
		for (int i = 0; i < objectsInRange.Count; i++) {
			Area2D item = objectsInRange[i] as Area2D;
			Vector2 itemLoc = item.GlobalPosition;
			float dis = pos.DistanceTo(itemLoc);
			if (dis < minDis) {
				minItem = item;
				minDis = dis;
			}
		}

		return minItem as IInteractable;
	}

	private void _on_area_entered(Area2D area) {
		if (area is IInteractable) {
			objectsInRange.Add(area);
			if (objectsInRange.Count == 1) {
				EmitSignal(SignalName.IsInteractable);
			}
		}
	}

	private void _on_area_exited(Area2D area) {

		if (area is IInteractable) {
			objectsInRange.Remove(area);
			if (objectsInRange.Count <= 0) {
				EmitSignal(SignalName.IsInteractable);
			
			}
		}
	}


}



