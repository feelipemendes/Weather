import React, { useState, useEffect } from 'react';
import axios from 'axios';

const App = () => {
  const [location, setLocation] = useState('');
  const [coordinatesList, setCoordinatesList] = useState([]); // Lista de coordenadas encontradas
  const [selectedCoordinates, setSelectedCoordinates] = useState(null); // Coordenadas selecionadas
  const [weatherData, setWeatherData] = useState(null); // Dados do clima
  const [bookmarkedLocations, setBookmarkedLocations] = useState([]); // Lista de cidades salvas e suas temperaturas

  // Função para converter Kelvin em Celsius
  const convertKelvinToCelsius = (kelvin) => {
    return (kelvin - 273.15).toFixed(2);
  };

  // Função para buscar coordenadas por nome de local
  const fetchCoordinates = async () => {
    try {
      const response = await axios.get(
        `https://localhost:7180/Weather/coordinates?LocationName=${location}`
      );
      setCoordinatesList(response.data); // Armazena a lista de coordenadas retornada
    } catch (error) {
      console.error('Error fetching coordinates', error);
    }
  };

  // Função para obter o clima baseado nas coordenadas selecionadas
  const fetchWeather = async (coordinates) => {
    try {
      const response = await axios.get(
        `https://localhost:7180/Weather/weather?lat=${coordinates.lat}&lon=${coordinates.lon}&LocationName=${coordinates.name}`
      );
      setWeatherData(response.data); // Armazena os dados do clima
      setSelectedCoordinates(coordinates); // Armazena as coordenadas selecionadas
    } catch (error) {
      console.error('Error fetching weather data', error);
    }
  };

  // Função para salvar a cidade como "bookmark" no banco de dados
  const saveBookmark = async () => {
    if (!weatherData || !selectedCoordinates) return;

    const locationBookmark = {
      locationName: weatherData.name,
      latitude: selectedCoordinates.lat,
      longitude: selectedCoordinates.lon,
      country: selectedCoordinates.country,
      state: selectedCoordinates.state,
    };

    try {
      await axios.post(
        'https://localhost:7180/Weather/locationbookmark',
        locationBookmark
      );
      fetchBookmarkedLocations(); // Atualiza a lista de bookmarks após salvar
    } catch (error) {
      console.error('Error saving location bookmark', error);
    }
  };

  // Função para buscar todas as cidades salvas no banco de dados
  const fetchBookmarkedLocations = async () => {
    try {
      const response = await axios.get(
        'https://localhost:7180/Weather/locationbookmarks'
      );

      const locations = response.data;

      // Para cada localização salva, buscar a temperatura
      const enrichedLocations = await Promise.all(
        locations.map(async (location) => {
          try {
            const weatherResponse = await axios.get(
              `https://localhost:7180/Weather/weather?lat=${location.latitude}&lon=${location.longitude}&LocationName=${location.locationName}`
            );

            // Adiciona os dados de temperatura ao local
            return {
              ...location,
              temperature: convertKelvinToCelsius(weatherResponse.data.main.temp),
              weatherDescription: weatherResponse.data.weather[0].description,
            };
          } catch (error) {
            console.error('Error fetching weather for bookmarked location', error);
            return { ...location, temperature: 'N/A', weatherDescription: 'N/A' };
          }
        })
      );

      setBookmarkedLocations(enrichedLocations);
    } catch (error) {
      console.error('Error fetching bookmarked locations', error);
    }
  };

  // Carregar as cidades salvas ao carregar o frontend
  useEffect(() => {
    fetchBookmarkedLocations();
  }, []);

  return (
    <div>
      <h1>Weather App</h1>

      {/* Campo de busca */}
      <div>
        <input
          type="text"
          placeholder="Enter city name"
          value={location}
          onChange={(e) => setLocation(e.target.value)}
        />
        <button onClick={fetchCoordinates}>Search</button>
      </div>

      {/* Lista de resultados para o usuário selecionar */}
      {coordinatesList.length > 0 && (
        <div>
          <h2>Select a location:</h2>
          <ul>
            {coordinatesList.map((coord, index) => (
              <li key={index}>
                <button onClick={() => fetchWeather(coord)}>
                  {coord.name}, {coord.state}, {coord.country}
                </button>
              </li>
            ))}
          </ul>
        </div>
      )}

      {/* Exibe os dados do clima se disponíveis */}
      {weatherData && (
        <div>
          <h2>Weather Information</h2>
          <p>Location: {weatherData.name}</p>
          <p>Temperature: {convertKelvinToCelsius(weatherData.main.temp)}°C</p>
          <p>Weather: {weatherData.weather[0].description}</p>
          <button onClick={saveBookmark}>Save Location</button>
        </div>
      )}

      {/* Exibe a lista de cidades salvas */}
      {bookmarkedLocations.length > 0 && (
        <div>
          <h2>Saved Locations</h2>
          <ul>
            {bookmarkedLocations.map((bookmark, index) => (
              <li key={index}>
                {bookmark.locationName}, {bookmark.state}, {bookmark.country} - 
                Temperature: {bookmark.temperature}°C, Weather: {bookmark.weatherDescription}
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
};

export default App;
