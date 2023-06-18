using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }

    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }

    public static Vector3 SetRandomPositionSphere(this GameObject go, float mindisatnce = 3f, float maxdistacne = 8f, float additionalHeighy = 5f, Transform TargetTransform = null)
    {

        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);

        // ������ �Ÿ��� ����
        float distance = UnityEngine.Random.Range(mindisatnce, maxdistacne);



        float _additionalHeight = additionalHeighy;

        //���Ӱ� ���� ��������, X,Z ��ǥ���� ������ �ݰ�, ������ ���� ���� ��
        //Y ��(����)�� ���� ��ġ ( �ݸ��� �߽��� + ���� ��*(�ݸ��� ���� ���ڸ�  + �߰����� �������� �ö󰥳���)
        Vector3 newPosition;
        if (TargetTransform != null)
        {
            Vector3 colliderSize = Vector3.zero;
            if (TargetTransform.TryGetComponent(out Collider collider))
            {
                colliderSize = collider.bounds.size;
            }

            // �ﰢ�Լ��� ����Ͽ� ��ġ ���
            float xPos = TargetTransform.position.x + Mathf.Cos(angle) * distance;
            float zPos = TargetTransform.position.z + Mathf.Sin(angle) * distance;
            Vector3 CirclePos = new Vector3(xPos, TargetTransform.position.y, zPos);
            newPosition = CirclePos + Vector3.up * (colliderSize.y * 0.5f + _additionalHeight);
            go.transform.position = newPosition;
            return newPosition;
        }
        else
        {
            Collider collider = go.GetComponent<Collider>();
            Bounds colliderBounds = collider.bounds;
            Vector3 colliderSize = colliderBounds.size;
            Vector3 colliderCenter = colliderBounds.center;
            // �ﰢ�Լ��� ����Ͽ� ��ġ ���
            float xPos = go.transform.position.x + Mathf.Cos(angle) * distance;
            float zPos = go.transform.position.z + Mathf.Sin(angle) * distance;
            Vector3 CirclePos = new Vector3(xPos, go.transform.position.y, zPos);
            newPosition = colliderCenter + CirclePos + Vector3.up * (colliderSize.y * 0.5f + _additionalHeight);
            go.transform.position = newPosition;
            return newPosition;
        }



    }


}


