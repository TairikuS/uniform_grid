using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviourScript : MonoBehaviour
{
    Vector3 velocity;
    const float speed = 3.0f;
    Material mat;
    GridBehaviourScript grid;
    bool is_lighting_ = false;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        SetLighting(is_lighting_);// SetLighting ���X�^�[�g�O�ɌĂ΂��\��������̂ŁA������ł��ݒ�

        grid = GameObject.Find("Grid").GetComponent<GridBehaviourScript>();

        float ang = 2 * Mathf.PI * Random.value;
        velocity.x = speed * Mathf.Cos(ang);
        velocity.y = 0.0f;
        velocity.z = speed * Mathf.Sin(ang);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(velocity * Time.deltaTime);
        grid.updatePosition(gameObject);// �O���b�h�ɍX�V��`����

        // ����
        const float floor_min = -0.5f * GridBehaviourScript.CELL_SIZE;
        const float floor_max = floor_min + GridBehaviourScript.CELL_SIZE;
        if (this.transform.position.x < floor_min && velocity.x < 0.0f) velocity.x = -velocity.x;
        if (floor_max < this.transform.position.x && 0.0f < velocity.x) velocity.x = -velocity.x;
        if (this.transform.position.z < floor_min && velocity.z < 0.0f) velocity.z = -velocity.z;
        if (floor_max < this.transform.position.z && 0.0f < velocity.z) velocity.z = -velocity.z;

        // ���x�͈���
        velocity.Normalize();
        velocity *= speed;
    }

    public void SetLighting(bool is_lighting)
    {
        if (!ReferenceEquals(mat, null))
        { 
            mat.SetFloat("_Intensity", is_lighting ? 1.0f : 0.0f);
            is_lighting_ = is_lighting;
        }
    }   

    // ���X�g�\���̊Ǘ�
    public GameObject prev_ = null;
    public GameObject next_ = null;
    public int cellX_, cellY_;
}
