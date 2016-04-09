// speed at which audio clip plays at its original pitch:
var audioClipSpeed = 10.0;


function Update() {
    var p = GetComponent.<Rigidbody>().velocity.magnitude / audioClipSpeed;
    GetComponent.<AudioSource>().pitch = Mathf.Clamp( p, 0.1, 4.0); // p is clamped to sane values
}