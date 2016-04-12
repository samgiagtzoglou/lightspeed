#pragma strict

var woosh1 : AudioClip;

function Start () {

}

function Update () {

}

function OnTriggerEnter () {

GetComponent.<AudioSource>().PlayOneShot(woosh1);
//GetComponent.<AudioSource>().PlayClipAtPoint(woosh1);

}