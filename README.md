# CapitalGStudios.Monogame.Examples
An suite of examples built upon the Monogame framework

# Requirements
Monogame 3.7.1

Visual Studio 2017

# The Quad
![The Quad](https://capitalgstudios.files.wordpress.com/2018/12/Quad.png)

# Overview
Currently this is a single example highlighting the performance difference between drawing 250,000 quads (500,000 triangles) as individual sprites with SpriteBatch and doing the same thing via a single VertexBuffer. This works smoothly at 60 FPS when using a VertexBuffer and runs quite poorly 10-15 FPS when using SpriteBatch. The quad image above is used in relation to the actual code implementation of this example and should be used for reference.

# Controls
F1 - Toggle between drawing using SpriteBatch or VertexBuffer

W,A,S,D - Pan the camera up/down/left/right

Q,E - Zoom in and out

Up Arrow - Increase the amount of quads to draw

Down Arrow - Decrease the amount of quads to draw

# Notes
When changing the amount of quads to draw (particularily when very high), there will be a noticeable lag in performance as the VertexBuffer is being re-created; this is generally not a good idea in production code but this is purely for learning.

Upon first running the application, you will be looking at 250,000 quads from very far away, you can zoom in with E and see the individual quads colored differently. Pressing F1 will switch to SpriteBatch and they use screen coordinates and the camera hasn't been implemented to move that around.
