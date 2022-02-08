# InventorySystem
Programmer assignment: Inventory system 

This is an asingment project that involves creating an funcioning inventory system, with intermidiate coplex interactions, an Equipment and stat screen and a demo scene that alowa the usage of it.

Controls:

Movement-wasd and arrow keys

Keyboard input:
	"i"-open inventory
	"c"-open stats
	"e"-open equipment
	
	"x"- add 1(one) Coin to your inventory
	"Space"- spawn a random object below your the player character
	
Mouse input:
	"Right Click on item"-Equip Equipable item
	"Middle Click on item"-Use Usable Item
	"Hold Left Click on item"
		-Drag item around:
						-Drop in inventory swaps/Add Stackable items/Puts on an empty slot 
						-Drop in game- spawns item in game and remove from invenotry
						-Drop in Equipment- if the item type is right for the slot then equip it and update stats

Mouse + Keyboard input:
	-"Right Click + v"-drops the item to the ground next to the player
	
	
	
Scripts:
	Item:	-Item ->class for all items, Scriptable Object
			-EquippableItem -> holds equipment types and stats of the item
			-UsableItem -> makse item usable, has list of Effects that are scriptable objects
					-UsableItemEffect-holds Use() funcion to execute effects
						-HealItemEffect -> effect for Usable Items
						
			-PermanentUsageItem -> item that cant be added to inventory, has a Description of its ussage
	Character Stats:
		-CharacterStats-Serialaziable class for stat funcionalty
		-StatModifier- readonly stuff for stats and fucnions to implement elsewhere
		
UI:
	-Character -> main script for all event funcionality of the Inventory. Like Drag,drop, mose clicks. Funcions that call on the inventory, item slot and Player
	-InfiniteInventory -> rework of the unsued Inventory script, that implaments dynamic behavior. Has funcionality like AddToInventory(), RemoveItem(), IsFull(). Every action is translatet to the item slot that it in use of an event.
	-ItemSlot -> Contains its item and its amount, all invokes of event that can happen when interacted with it
		-EquipmentSlot -> extends ItemSlot, funcionlity with equipment
	
	-ItemTooltip -> Show and Hide the tooltip
	-StatDisplay -> Update and Set stats
	-EquippmentPanel -> event for clicking, dragin, removing and adding equipment
	-"note" every other script is used for UI interactions, like opening and closing, catching drop event, unsed like the old Inventory script
	
InteractionsWithScene:
	-ItemInWorld -> Get Set and oer funcionality to use on a prefeb of items in game
	-PickUpItem -> Input interactions with items with the player
	-SpawnIndefinitly
	-AddCoins
	

			
			
