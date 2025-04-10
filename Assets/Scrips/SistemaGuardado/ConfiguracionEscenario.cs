using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //para no repetir el system.serializble

[Serializable]
public class ConfiguracionEscenario
{
    public string nombreEscenario;

    public Vector3 posicionJugador;
    public Quaternion rotacionJugador;

    public List<DatosLuz> luces = new();
    public List<ObjetoInteractivo> objetos = new();

    public List<DatosLuzExtra> valoresLuz = new();
}

[Serializable]
public class DatosLuz
{
    public Vector3 posicion;
    public Quaternion rotacion;
    public float intensidad;
    public Color color;
}

[Serializable]
public class ObjetoInteractivo
{
    public string nombre;
    public Vector3 posicion;
    public Quaternion rotacion;
    public Vector3 escala;
}

[Serializable]
public class DatosLuzExtra
{
    public float intensidad;
    public float temperatura;
    public float rotacionY;
    public float angulo;
    public string tipoPreset;
}


