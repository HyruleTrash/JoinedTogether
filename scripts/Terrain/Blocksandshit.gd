extends TileMap

var timer = false
var animate_forwards = true
var animate_forwards2 = true
var animate_forwards3 = true


func _physics_process(delta):
	if timer == true:
		for cell in get_used_cells_by_id(2):
			var tile = get_cell_autotile_coord(cell.x, cell.y)
			if int(tile.x + 1) %5 == 0 and animate_forwards:
				animate_forwards = false
			if int(tile.x) %5 == 0 and !animate_forwards:
				animate_forwards = true
			
			if animate_forwards:
				set_cell(cell.x, cell.y, 2, false, false, false, Vector2(tile.x + 1, tile.y))
			if !animate_forwards:
				set_cell(cell.x, cell.y, 2, false, false, false, Vector2(tile.x - 1, tile.y))
		for cell in get_used_cells_by_id(3):
			var tile = get_cell_autotile_coord(cell.x, cell.y)
			if tile.x + 1 == 6 and animate_forwards2:
				animate_forwards2 = false
			if tile.x - 1 == 0 and !animate_forwards2:
				animate_forwards2 = true
			
			if animate_forwards2:
				set_cell(cell.x, cell.y, 3, false, false, false, Vector2(tile.x + 1, tile.y))
			if !animate_forwards2:
				set_cell(cell.x, cell.y, 3, false, false, false, Vector2(tile.x == 0, tile.y))
		for cell in get_used_cells_by_id(4):
			var tile = get_cell_autotile_coord(cell.x, cell.y)
			if tile.x + 1 == 6 and animate_forwards2:
				animate_forwards2 = false
			if tile.x - 1 == 0 and !animate_forwards2:
				animate_forwards2 = true
			
			if animate_forwards2:
				set_cell(cell.x, cell.y, 4, false, false, false, Vector2(tile.x + 1, tile.y))
			if !animate_forwards2:
				set_cell(cell.x, cell.y, 4, false, false, false, Vector2(tile.x == 0, tile.y))
		for cell in get_used_cells_by_id(5):
			var tile = get_cell_autotile_coord(cell.x, cell.y)
			if tile.x + 1 == 8 and animate_forwards3:
				animate_forwards3 = false
			if tile.x - 1 == 0 and !animate_forwards3:
				animate_forwards3 = true
			if Infoholder.stategirl:
				if animate_forwards3:
					set_cell(cell.x, cell.y, 5, false, false, false, Vector2(tile.x + 1, tile.y))
				if !animate_forwards3:
					set_cell(cell.x, cell.y, 5, false, false, false, Vector2(tile.x - 1, tile.y))
			else:
				set_cell(cell.x, cell.y, 6, false, false, false, Vector2(tile.x, tile.y))
		for cell in get_used_cells_by_id(6):
			var tile = get_cell_autotile_coord(cell.x, cell.y)
			if tile.x + 1 == 8 and animate_forwards3:
				animate_forwards3 = false
			if tile.x - 1 == 0 and !animate_forwards3:
				animate_forwards3 = true
			if !Infoholder.stategirl:
				if animate_forwards3:
					set_cell(cell.x, cell.y, 6, false, false, false, Vector2(tile.x + 1, tile.y))
				if !animate_forwards3:
					set_cell(cell.x, cell.y, 6, false, false, false, Vector2(tile.x - 1, tile.y))
			else:
				set_cell(cell.x, cell.y, 5, false, false, false, Vector2(tile.x + 1, tile.y))
		
		timer = false
		


func _on_Timer_timeout():
	timer = true
