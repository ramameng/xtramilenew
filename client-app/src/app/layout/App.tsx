import React, { SyntheticEvent } from 'react';
import { Container, DropdownProps, Form, Header, List, Segment } from 'semantic-ui-react';
import { useState } from 'react';
import { useEffect } from 'react';
import axios from 'axios';
import { Country } from '../../models/country';
import { City } from '../../models/city';
import Navbar from './Navbar';
import moment from 'moment';

function App() {
  const [cityOptions, setCityOptions] = useState([]);
  const [countriesOptions, setCountriesOptions] = useState([]);
  const [weather, setWeather] = useState<any>([]);

  useEffect(() => {
    axios.get('http://localhost:5000/api/countries').then(response => {
      setCountriesOptions(
        response.data.map((x: Country) => {
        return { key : x.id, text: x.name, value: x.id}
      }));
    })
  }, [])

  const handleSelectChangeCountries = (event: SyntheticEvent<HTMLElement,Event>, data: DropdownProps) => {
    axios.get('http://localhost:5000/api/cities/' + data.value).then(response => {
      setCityOptions(
        response.data.map((x: City) => {
        return { key : x.id, text: x.name, value: x.name}
      }));
    })
  }

  const handleSelectChangeCities = (event: SyntheticEvent<HTMLElement,Event>, data: DropdownProps) => {
    axios.get('https://api.openweathermap.org/data/2.5/weather?q=' + data.value + '&appid=059ce7ef4743d449900565b6524c5862').then(response => {
      setWeather(response.data);
      console.log(response.data);
    });
  }

  return (
    <div>
      <Navbar />
      <Segment clearing></Segment>
      <Container>
        <Segment clearing>
          <Form>
            <Form.Select
              placeholder='Select Country'
              search
              selection
              options={countriesOptions}
              onChange={(e,data) => handleSelectChangeCountries(e,data)}
            />
            <Form.Select
              placeholder='Select City'
              search
              selection
              options={cityOptions}
              onChange={(e,data) => handleSelectChangeCities(e,data)}
            />
          </Form>
        </Segment>
        {(typeof weather.main != 'undefined') ? (
          <Segment clearing>
          <Header as='h1'>Weather Forecast</Header>
          <List>
          <List.Item icon='marker' content={weather.name} />
          <List.Item icon='time' content={moment(weather.dt!).format('MMMM Do YYYY, h:mm:ss a')} />
          <List.Item><h2>{(weather.main.temp - 273.15).toLocaleString(undefined, {maximumFractionDigits:2})}</h2></List.Item>
          <List.Item>{weather.weather[0].main}, {weather.weather[0].description}</List.Item>
          <List.Item>Wind Speed: {weather.wind.speed} m/s</List.Item>
          <List.Item>Visibility: {weather.visibility}</List.Item>
          {/* i dont know which one is dew point */}
          <List.Item>Dew Point:</List.Item>
          <List.Item>Relative Humidity: {weather.main.humidity}</List.Item>
          <List.Item>Pressure: {weather.main.pressure}</List.Item>
          </List>
        </Segment>
        ): (
          <div></div>
        )}
      </Container>
    </div>
  );
}

export default App;
