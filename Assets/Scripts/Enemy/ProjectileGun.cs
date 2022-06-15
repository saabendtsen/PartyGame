using UnityEngine;
using UnityEngine.AI;


public class ProjectileGun : MonoBehaviour
{


    //bullet
    public GameObject bullet;

    //GunOwner
    //[SerializeField] private Rigidbody agent;

    //playerPos
    private GameObject playerObj = null;
    private Vector3 playerPosition;
    public float targetRange;

    //bullet force
    public float shootForce, upwardForce;

    //gun Stats
    public float timeBetweenShooting, spread, reloadTime, TimeBEtweenshots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;


    //Recoil
    private float recoilForce = 2;


    public bool allowInvoke = true;

    //graphics 
    public GameObject muzzelFlash;


    private void Start()
    {

        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }

        bulletsLeft = magazineSize;
        readyToShoot = true;
    }


    void Update()
    {
        //update PlayerPosition
        //playerPosition = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z);

        //MyInput(playerPosition);

        
    }

    public void MyInput(Vector3 target)
    {

        if(readyToShoot && !reloading && bulletsLeft <= 0) Reload();

        //Shooting 
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot(target);
        }
    }
    private void Shoot(Vector3 target)
    {
        readyToShoot = false;
        Vector3 directionWithoutSpread = target - transform.position;

        //calc spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calc dir with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;

        //Recoil
        //agent.AddForce(-directionWithSpread.normalized * recoilForce,ForceMode.Impulse);

        //force to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(target * shootForce,ForceMode.Impulse);


        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", TimeBEtweenshots);
        }


    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}