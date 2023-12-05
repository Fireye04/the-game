using Godot;
using System;

public partial class DuckHitBox : Area2D, IInteractable
{
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
			GD.Print("quack");
		} else if (choice == 9) {
			GD.Print("The FitnessGram Pacer Test is a multistage aerobic capacity test that progressively gets more difficult as it continues. The 20 meter pacer test will begin in 30 seconds. Line up at the start. The running speed starts slowly but gets faster each minute after you hear this signal bodeboop. A sing lap should be completed every time you hear this sound. ding Remember to run in a straight line and run as long as possible. The second time you fail to complete a lap before the sound, your test is over. The test will begin on the word start.");
		} else {
			GD.Print("Capitalism was a mistake, long live the revolution");
		}
	}
	
}
