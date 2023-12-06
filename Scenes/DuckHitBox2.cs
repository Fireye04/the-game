using Godot;
using System;

public partial class DuckHitBox2 : Area2D, IInteractable
{


	[Export]
	public string itemName;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void Interact(){
		Random rnd = new Random();
		int choice = rnd.Next(1,11);
		if (choice < 9){
			GD.Print("not quack");
		} else if (choice == 9) {
			GD.Print("Take 3d10 necrotic damage");
		} else {
			GD.Print("Would you rather fight 100 duck sized horses or 10 horse sized ducks");
		}
	}

	public string getItemName()
	{
		return itemName;
	}
}
