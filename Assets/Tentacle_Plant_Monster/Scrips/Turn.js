#pragma strict
var speed : float;

    function Update() 
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.up * (Time.deltaTime * speed));
    }
