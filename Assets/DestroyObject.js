#pragma strict

function OnCollisionEnter (col : Collision)
{
    if(col.gameObject.name == "powerup_pickup")
    {
    col.gameObject.SetActive(false);
    }
}