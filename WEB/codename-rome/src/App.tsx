import './App.css';
import Header from './components/Header/Header';
import Menu from './components/Menu/Menu'
import { getMenu } from './common/connection/Connection';
import { useEffect, useState } from 'react';
import { MenuItem } from './common/types/MenuItem';

function App() {
  const [menu, setMenu] = useState<MenuItem[]>([])

  useEffect(() => {
    getMenu().then(data => setMenu(data))
  }, [])
  
  return (
    <div>
      <Header/>
      <Menu menu = {menu}/>
    </div>
  );
}

export default App;
