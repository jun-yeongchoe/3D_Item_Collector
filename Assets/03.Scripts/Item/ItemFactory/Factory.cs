using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    public abstract IProduct GetProduct(Vector3 pos);

    public string GetLog(IProduct product)
    {
        string logMsg = "Factory : Created Product" + product.ProductName;
        return logMsg;
    }

}
