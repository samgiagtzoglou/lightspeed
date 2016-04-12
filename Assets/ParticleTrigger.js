 var particle : ParticleSystem;


function Start () {
particle.Stop();
}


 function OnTriggerEnter(col:Collider)
 {
   
        particle.Play();

 }
