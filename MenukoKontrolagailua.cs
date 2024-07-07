using Godot;
using System;

public partial class MenukoKontrolagailua : Node
{
	public override void _Process(double delta)
	{
		if(Multiplayer.GetPeers().Length == 1)
			GetTree().ChangeSceneToFile("res://gela.tscn");
	}

	private void _on_zerbitzaria_sortu_pressed()
	{
		TextEdit portuaNode = GetNode("./ZerbitzariPortu") as TextEdit;
		int portua = portuaNode.Text.ToInt();

		ENetMultiplayerPeer peer = new ENetMultiplayerPeer();
		peer.CreateServer(portua, 1); // bezero bat gehienez (2 jokalari partidan)

		Multiplayer.MultiplayerPeer = peer;
	}

	private void _on_zerbitzarira_konektatu_pressed()
	{
		TextEdit zerbitzariIPNode = GetNode("./ZerbitzariIP") as TextEdit;
		TextEdit zerbitzariPortuNode = GetNode("./ZerbitzariPortu") as TextEdit;

		String zerbitzariIP = zerbitzariIPNode.Text;
		int zerbitzariPortu = zerbitzariPortuNode.Text.ToInt();

		ENetMultiplayerPeer peer = new ENetMultiplayerPeer();
		peer.CreateClient(zerbitzariIP, zerbitzariPortu);

		Multiplayer.MultiplayerPeer = peer;
	}
}
