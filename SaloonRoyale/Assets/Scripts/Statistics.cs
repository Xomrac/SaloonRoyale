using System;
using System.Collections;
using System.Collections.Generic;
using CardSystem;
using Sequencing.Points;
using UnityEngine;

public class Statistics : MonoBehaviour
{

	private int valueCardUsed = 0;
	private int valueEnemyDefeated = 0;
	private int valueEnemyTotalInGame;

	private EnemyPoint[] enemyTotalInGame;
	private List<EnemyPoint> enemyDefeatedList;
	
	private Deck playerDeck;
	private DeckHolder deckHolder;

	private void OnEnable()
	{
	//	deckHolder.OnCardPlayed += OnCardUsed();
	}	
	
	
	private void Start()
	{
	enemyTotalInGame = FindObjectsOfType<EnemyPoint>();
	valueEnemyTotalInGame = enemyTotalInGame.Length;

	deckHolder = FindObjectOfType<DeckHolder>();
	playerDeck = FindObjectOfType<DeckHolder>().PlayerDeck;
	
	}

	public void OnEnemyBeingKilled(EnemyPoint enemykilled)
	{
		enemyDefeatedList.Add(enemykilled);
		
		valueEnemyDefeated++;
	}
	
	public void OnCardUsed()
	{
		valueCardUsed++;
	}
	
	private void OnDisable()
	{
	//	deckHolder.OnCardPlayed -= OnCardUsed();
	}

	
}
