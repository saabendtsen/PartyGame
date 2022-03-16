using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMpro;

public class ProjectileScript : MonoBehaviour
{
    //Bullet
    public GameObject bullet;
    
    //Bullet force
    public float shootForce;
    public float upwardForce;
    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magzineSize, bulletsPerTap;
    public bool allowButtonHold = true;

    int bulletsLeft, bulletsShot;

    //Bools
    bool shooting, readyToShoot, reloading;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    //Bug fix
    public bool allowInvoke = true;

    private void Awake()
    {
        //make sure magizine is full
        bulletsLeft = magzineSize;
        readyToShoot = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

        //Set ammo display, if it exits
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magzineSize / bulletsPerTap);
    }

    private void MyInput()
    {
        //check if allowed to hold down button and take corresponding input
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading
        if(Input.GetKeyDown(keyCode.R) && bulletsLeft < magzineSize && !reloading) Reload();
        //Reload automatically when trying to shoot without ammo
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Shooting
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullet shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        //Calculate the position from attackpoint to targetpoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y,0);

        //Instantiate Bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); 
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add force to the bullet
        currentBullet.GetComponent<RigidBody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<RigidBody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
         
        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
    
        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked)
        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = ready;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadingFinished", reloadTime);
    }

    private void ReloadingFinished()
    {
        bulletsLeft = magzineSize;
        reloading = false;
    }
}
