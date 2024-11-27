import React from 'react';

const WeatherInfo = ({ weatherData, onSaveBookmark }) => {
  if (!weatherData) return null;

  return (
    <div>
      <h2>Weather Info for {weatherData.name}</h2>
      <p>Temperature: {weatherData.main.temp}°C</p>
      <p>Weather: {weatherData.weather[0].description}</p>
      <button onClick={() => onSaveBookmark(weatherData)}>Save as Bookmark</button>
    </div>
  );
};

export default WeatherInfo;
