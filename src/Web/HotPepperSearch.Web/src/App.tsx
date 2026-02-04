import { SearchPage } from './pages/SearchPage';
import './App.css';

function App() {
  return (
    <div className="app">
      <header>
        <h1>ホットペッパービューティー検索</h1>
      </header>
      <main>
        <SearchPage />
      </main>
    </div>
  );
}

export default App;
