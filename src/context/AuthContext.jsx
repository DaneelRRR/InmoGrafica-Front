import { createContext, useState, useContext, useEffect } from 'react';
import { getRoleStrategy } from '../strategies/roleStrategies';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [strategy, setStrategy] = useState(null);

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      const parsedUser = JSON.parse(storedUser);
      setUser(parsedUser);
      setStrategy(getRoleStrategy(parsedUser.rol));
    }
  }, []);

  const login = (userData, token) => {
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify(userData));
    setUser(userData);
    setStrategy(getRoleStrategy(userData.rol));
  };

  const logout = () => {
    localStorage.clear();
    setUser(null);
    setStrategy(null);
  };

  return (
    <AuthContext.Provider value={{ user, strategy, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);