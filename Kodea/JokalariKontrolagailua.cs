using Godot;
using System;

public partial class JokalariKontrolagailua : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (this.Name == "jokalaria1")
		{
			CallDeferred("set_multiplayer_authority", 1);
		}
		else
		{
			if (Multiplayer.IsServer())
				CallDeferred("set_multiplayer_authority", Multiplayer.GetPeers()[0]);
			else
				CallDeferred("set_multiplayer_authority", Multiplayer.GetUniqueId());
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (IsMultiplayerAuthority())
		{
			if (Input.IsActionPressed("ezkerreraMugitu"))
				Position -= new Vector2(1, 0);

			if (Input.IsActionPressed("eskubiraMugitu"))
				Position += new Vector2(1, 0);

			if (Input.IsActionPressed("txatIreki"))
			{
				TextEdit mezua = GetNode("../TxatTestua") as TextEdit;
				mezua.Visible = true;
			}
			if(Input.IsActionPressed("mezuaBidali"))
			{
				TextEdit mezuaNodoa = GetNode("../TxatTestua") as TextEdit;
				if(mezuaNodoa.Visible && mezuaNodoa.Text != "")
				{
					Rpc("MezuaBidali", Name, mezuaNodoa.Text);
					mezuaNodoa.Text = "";
					mezuaNodoa.Visible = false;
				}
			}
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable, CallLocal = true)]

	private void MezuaBidali(String jokalaria, String mezua)
	{
		(GetNode("../TxatOsoa") as RichTextLabel).Text += jokalaria + ": " + mezua + "\n";
		(GetNode("../TxatOsoa") as RichTextLabel).Visible = true;
	}
}
