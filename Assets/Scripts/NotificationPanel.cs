using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPanel : MonoBehaviour
{
    public void AddNotification(Notification notification, string msg)
    {
        if (transform.childCount > 0)
        {
            var existing = transform.GetChild(0);
            Destroy(existing.gameObject);
        }
        notification.transform.SetParent(this.transform,false);
        notification.ShowMessage(msg);
    }
}
