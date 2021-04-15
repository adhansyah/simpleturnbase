import random
import copy

class Vector2:
    def __init__(self, x, y):
        self.x = x
        self.y = y

    @staticmethod
    def add(va, vb):
        return Vector2(va.x + vb.x, va.y + vb.y)

    @staticmethod
    def subtract(va, vb):
        return Vector2(vb.x - va.x, vb.y - va.y)

    @staticmethod
    def distance(va, vb):
        return Vector2(abs(vb.x - va.x), abs(vb.y - va.y))

    def __eq__(self, v):
        if self.x == v.x and self.y == v.y:
            return True
        return False

    def __repr__(self):
        return f'({self.x}, {self.y})'

left = Vector2(-1, 0)
right = Vector2(1, 0)
up = Vector2(0, -1)
down = Vector2(0, 1)

width = 10
height = 20

start_x = 5 # y = 0 ofc
end_x = 5 # y = height ofc

start_pos = Vector2(start_x, 0)
end_pos = Vector2(end_x, height - 1)

arr = [[0 for x in range(width)] for y in range(height)]
visited = [[False for x in range(width)] for y in range(height)]

arr[start_pos.y][start_pos.x] = 1
visited[start_pos.y][start_pos.x] = True

path = {}

current_i = 1

def movable(pos):
    for d in [up, right, down, left]:
        p = Vector2.add(pos, d)
        if p.x < 0 or p.x >= width:
            continue
        if p.y < 0 or p.y >= height:
            continue
        if visited[p.y][p.x]:
            continue
        return True
    return False

def go(current_pos):
    global current_i
    arr[current_pos.y][current_pos.x] = current_i
    if current_i in path:
        path[current_i].append(current_pos)
    else:
        path[current_i] = [current_pos]
    if current_pos.y == end_pos.y:
        if current_pos.x == end_pos.x:
            return
        else:
            distance = Vector2.distance(current_pos, end_pos).x
            if current_pos.x < end_pos.x:
                i = 1
            else:
                i = -1
            for d in range(1, distance + 1):
                pos = Vector2.add(current_pos, Vector2(i * d, 0))
                if visited[pos.y][pos.x]:
                    break
                path[current_i].append(pos)
                arr[pos.y][pos.x] = current_i
                visited[pos.y][pos.x] = True
        return
    directions = [up, right, down, left]
    while len(directions) > 0:
        direction = random.choice(directions)
        new_pos = Vector2.add(current_pos, direction)
        if new_pos.x < 0 or new_pos.x >= width or \
            new_pos.y < 0 or new_pos.y >= height or \
            visited[new_pos.y][new_pos.x]:
            directions.remove(direction)
            continue
        visited[new_pos.y][new_pos.x] = True
        go(new_pos)
        if not movable(new_pos):
            continue
        current_i += 1
        directions.remove(direction)

go(start_pos) 

# while len(queue_pos) > 0:
#     current_pos = queue_pos.pop()
#     current_i = queue_i.pop()

#     dcount = 0
#     directions = [up, left, down, right]
#     while len(directions) > 0:
#         direction = random.choice(directions)
#         new_pos = Vector2.add(current_pos, direction)
#         if new_pos.x < 0 or new_pos.x >= width or \
#             new_pos.y < 0 or new_pos.y >= height or \
#             visited[new_pos.y][new_pos.x]:
#             directions.remove(direction)
#             continue
#         arr[new_pos.y][new_pos.x] = current_i
#         dcount += 1
#         visited[new_pos.y][new_pos.x] = True

#         queue_pos.append(new_pos)
#         queue_i.append(current_i)

#         if current_i in path:
#             path[current_i].append(new_pos)
#         else:
#             path[current_i] = [new_pos]
    
#     if dcount == 0:
#         queue_i[-1] += 1

print(path)