Kaelmix Studio - Game Assets
------------------------------

Thank you for purchasing this package!
If you have any questions, feature requests or if you have found a bug then please send an email to kaelmixstudio@gmail.com


HOW TO:
------------------------------
1. Apply the choosen material to your character
2. Tweek the parameters of the material
   2.1. Usually the parameters have their name starting with "Effect "
3. Change the parameter "Effect Value" via your script
   3.1. exemple:
         SkinnedMeshRenderer mesh = GetComponentInChildren<SkinnedMeshRenderer>();
         Material mat = mesh.material;
         mat.SetFloat("_EffectValue", 0.5f);


DEMO SCENE:
------------------------------
There is a demo scene located under the _Demo folder.
It contains all possible examples that you can make with this package.


CHANGELOG:
------------------------------
1.2:
New effects!
- Blink
- Electricty
- Fire
- Shield

1.1:
Fix an issue on some shaders
Shaders can now be applied to other elements than characters
NB: this package still mainly focus on characters and effects on other elements than characters may not be as nice as on the characters.

1.0:
First release