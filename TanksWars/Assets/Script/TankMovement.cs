using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankMovement : MonoBehaviour
{
    public GameObject Player;
    public Joystick joystick;
    public GameObject crossHair;
    public Joystick joystickAim;
    public Button fire;
    public Slider speed;
    public Rigidbody2D missile;
    public float moveSpeed = 10f;
    bool _faceRight = true;
    Rigidbody2D _tankRb;

    private void Awake()
    {
        _tankRb = Player.GetComponent<Rigidbody2D>();
        fire.onClick.AddListener(ShootMissile);
    }

    void Update()
    {
        float _move = joystick.Horizontal;
        if (joystick.Horizontal < 0 && _faceRight)
        {
            flip();
        }
        if (joystick.Horizontal > 0 && !_faceRight)
        {
            flip();
        }
        if (_move > 0.15f || _move < -0.15f)
        {
            _tankRb.velocity = new Vector2(_move * moveSpeed, _tankRb.velocity.y);
        }
        else
        {
            _tankRb.velocity = Vector2.zero;
        }
        moveCrossHair();
    }

    void flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(Vector3.up * 180f);
    }

    void moveCrossHair()
    {
        Vector2 aim = new Vector2(joystickAim.Horizontal, joystickAim.Vertical);
        if (aim.magnitude > 0f)
        {
            aim = aim.normalized;
            aim *= 2f;
            if (_faceRight)
            {
                crossHair.transform.localPosition = aim;
                crossHair.SetActive(true);
            }
            if (!_faceRight)
            {
                crossHair.transform.localPosition = Vector3.Reflect(aim, Vector3.left).normalized * 2f;
                crossHair.SetActive(true);
            }
            moveTureet();
        }
        else
        {
            crossHair.SetActive(false);
        }
    }

    void moveTureet()
    {
        Vector2 lookDir = crossHair.transform.position - Player.transform.GetChild(0).position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (angle > 90f && _faceRight && joystickAim.Horizontal < 0f)
        {
            flip();
        }
        if (angle < 90f && !_faceRight && joystickAim.Horizontal > 0f)
        {
            flip();
        }
        Player.transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void ShootMissile()
    {
        Vector2 lookDir = crossHair.transform.position - Player.transform.GetChild(0).position;
        Rigidbody2D missileInstance = Instantiate(missile, transform.GetChild(0).GetChild(0).position, transform.GetChild(0).rotation);
        missileInstance.velocity = new Vector2(lookDir.x * speed.value, lookDir.y * speed.value);
    }
}