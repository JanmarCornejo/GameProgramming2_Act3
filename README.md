<GAME PROG 2 - ACTIVITY 3>

Contributors:

- Geoffrey Mendoza

- Janmar Cornejo

ACTIVITY TO BE DONE:

Create an Inventory System with UI.

Each Item block consumes 1 square.

There are 3 Item types:

// Equipment

- cannot be stacked

- when double clicked will debug equipment name and description

// Misc

- cannot be stacked

- when double clicked will do nothing

// Consumable

- can be stacked

- when double clicked will debug name and description and be consumed

// Things you can do in the inventory system

- Reorganize inventory by dragging and dropping items.

// Drag

- drag will make a sprite go on top of the mouse cursor and follow it around until drag ends
- if drag ends outside the inventory, it becomes a drop
- if drag ends on top of an inventory slot, transfer inventory to that slot.

// Drop

- dropping provides a prompt if you want to drop item
- pressing cancel drop returns item back in inventory
- pressing drop removes item from inventory (as if it were consumed (including equipment))
 

 

Database:

Create an Item Database:

Item should be via scriptable object

Items must have:

designated sprite
id
description
Scene:

 

Scene must be of the Inventory System UI:

 

inventory system must have a debug UI to be able to add item via their itemID.

add a button to add ID as well as a input field to accept ids to add.

make sure to handle any errors if id is incorrect. code must not break.

------------------------------------------------------------------------------------------------
