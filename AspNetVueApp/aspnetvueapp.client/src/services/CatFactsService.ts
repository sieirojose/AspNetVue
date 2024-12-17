import axios from 'axios';

const API_URL = 'https://localhost:7258/CatFacts/all-facts';

export const getCatFacts = async () => {
  try {
    const response = await axios.get(API_URL, {
      withCredentials: false, 
    });
    console.log('API Response:', response.data); 
    return response.data;
  } catch (error) {
    console.error('Error fetching cat facts:', error);
    return [];
  }
};

