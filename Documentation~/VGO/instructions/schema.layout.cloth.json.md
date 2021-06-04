# VGO

## Cloth schema

### vgo.cloth

The cloth.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|name|The name of the object.|string||||
|enabled|Whether this component is enabled.|bool||true / false|true|
|damping|Damp cloth motion.|float||[0.0, 1.0]|0.0|
|useGravity|Should gravity affect the cloth simulation?|bool||true / false|true|
|friction|The friction of the cloth when colliding with the character.|float||[0.0, 1.0]|0.0|
|collisionMassScale|How much to increase mass of colliding particles.|float||[0.0, ]|0.0|
|enableContinuousCollision|Enable continuous collision to improve collision stability.|bool||true / false|true|
|useVirtualParticles|Add one virtual particle per triangle to improve collision stability.|float||[0.0, ]|0.0|
|worldVelocityScale|How much world-space movement of the character will affect cloth vertices.|float|||0.0|
|worldAccelerationScale|How much world-space acceleration of the character will affect cloth vertices.|float|||0.0|
|clothSolverFrequency|Number of cloth solver iterations per second.|float||[1.0, ]|1.0|
|useTethers|Use Tether Anchors.|bool||true / false|true|
|stiffnessFrequency|Sets the stiffness frequency parameter.|float|||0.0|
|selfCollisionDistance|Minimum distance at which two cloth particles repel each other.|float|||0.0|
|selfCollisionStiffness|Self-collision stiffness defines how strong the separating impulse should be for colliding particles.|float|||0.0|
|randomAcceleration|A random, external acceleration applied to the cloth.|float[3]||[x, y, z]||
|externalAcceleration|A constant, external acceleration applied to the cloth.|float[3]||[x, y, z]||
|stretchingStiffness|Stretching stiffness of the cloth.|float||[0.0, 1.0]|0.0|
|bendingStiffness|Bending stiffness of the cloth.|float||[0.0, 1.0]|0.0|
|sleepThreshold|Cloth's sleep threshold.|float||[0.0, ]|0.0|
|sphereColliders|An array of ClothSphereColliderPairs which this Cloth instance should collide with.|vgo.clothSphereColliderPair[]||||
|capsuleColliders|An array of CapsuleColliders which this Cloth instance should collide with.|int[]||||
|coefficients|The resource accessor index of the cloth skinning coefficients used to set up how the cloth interacts with the skinned mesh.|int|||-1|

### vgo.clothSphereColliderPair

The pair of the sphere collider.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|first|The index of the first SphereCollider.|int|||-1|
|second|The index of the second SphereCollider.|int|||-1|

___
Last updated: 5 June, 2021  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
