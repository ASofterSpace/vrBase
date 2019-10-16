/**
 * Unlicensed code created by A Softer Space, 2019
 * www.asofterspace.com/licenses/unlicense.txt
 */

using System.Collections.Generic;
using System.Collections;
using System;

using UnityEngine;


public class TicTacToeCtrl {

	private GameObject hostRoom;


	public TicTacToeCtrl(MainCtrl mainCtrl, GameObject hostRoom) {

		this.hostRoom = hostRoom;
	}

	public void createPlayingField(Vector3 position, Vector3 angles) {

		GameObject ticTacToe = new GameObject("TicTacToe");
		ticTacToe.transform.parent = hostRoom.transform;

		GameObject[][] fields = new GameObject[3][];
		for (int x = 0; x < 3; x++) {
			fields[x] = new GameObject[3];
			for (int y = 0; y < 3; y++) {
				fields[x][y] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
				fields[x][y].name = ButtonCtrl.BTN_TICTACTOE_FIELD + x + "-" + y;
				fields[x][y].transform.parent = ticTacToe.transform;
				fields[x][y].transform.localPosition = new Vector3((x - 1) * 0.7f, 0.025f, (y - 1) * 0.7f);
				fields[x][y].transform.eulerAngles = new Vector3(0, 0, 0);
				fields[x][y].transform.localScale = new Vector3(0.55f, 0.05f, 0.55f);
				MaterialCtrl.setMaterial(fields[x][y], MaterialCtrl.OBJECTS_TICTACTOE_GRAY);
			}
		}

		ticTacToe.transform.localPosition = position;
		ticTacToe.transform.eulerAngles = angles;
	}
}
