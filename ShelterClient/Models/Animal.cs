using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShelterClient.Models
{
  public class Animal
  {
    public int AnimalId { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }
    public int Age { get; set; }
    public IFormFile Image { get; set; }

    public static Animal[] GetAnimals()
    {
      Task<string> apiCallTask = ApiHelper.GetAll();
      string result =  apiCallTask.Result;
      
      JArray jsonResponse = JArray.Parse(result);
      List<Animal> animalList = JsonConvert.DeserializeObject<List<Animal>>(jsonResponse.ToString());

      return animalList.ToArray();
    }

    public static Animal GetDetails(int id)
    {
      var apiCallTask = ApiHelper.Get(id);
      var result = apiCallTask.Result;

      JObject jsonResponse = JObject.Parse(result);
      Animal animal = JsonConvert.DeserializeObject<Animal>(jsonResponse.ToString());

      return animal;
    }

    public static void Post(Animal animal)
    {
      string jsonAnimal = JsonConvert.SerializeObject(animal);
      ApiHelper.Post(jsonAnimal);
    }

    public static void Put(Animal animal)
    {
      string jsonAnimal = JsonConvert.SerializeObject(animal);
      ApiHelper.Put(animal.AnimalId, jsonAnimal);
    }

    public static void Delete(int id)
    {
      ApiHelper.Delete(id);
    }
  }
}