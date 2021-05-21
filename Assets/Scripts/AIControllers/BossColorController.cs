using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossColorController : MonoBehaviour
{

    public MeshRenderer meshRenderer;

    public Material whiteMaterial;
    public Material redMaterial;

    public GameObject lava;

    private float timer;
    private int timerSeconds;
    private int lastTimerSeconds;
    private bool red = false;

    private void Update()
    {

        lastTimerSeconds = timerSeconds;
        timer += Time.deltaTime;
        timerSeconds = (int) (timer % 60);

        if (lastTimerSeconds != timerSeconds)
        {
            //Seconds Changed;
            if (timerSeconds == 20) ChangeColor();
            else if (timerSeconds == 21) MoveLava();
            else if (timerSeconds == 22) MoveLava();
            else if (timerSeconds == 23) MoveLava();
            else if (timerSeconds == 24) MoveLava();
            else if (timerSeconds == 25) MoveLava();
            else if (timerSeconds == 26) MoveLava();
            else if (timerSeconds == 27) MoveLava();
            else if (timerSeconds == 28) MoveLava();
            else if (timerSeconds == 29) MoveLava();
            else if (timerSeconds == 30) MoveLava();
            else if (timerSeconds >= 20) timer = 0f;
        }

    }

    private void MoveLava()
    {
        float dY;
        if (red) dY = 0.28f;
        else dY = -0.28f;
        Vector3 lavaPos = lava.transform.position;
        lavaPos.y += dY;
        lava.transform.position = lavaPos;
    }

    private void ChangeColor()
    {
        red = !red;
        Material[] materials = meshRenderer.materials;
        if (red) materials[0] = redMaterial;
        else materials[0] = whiteMaterial;

        meshRenderer.materials = materials;

    }

}
