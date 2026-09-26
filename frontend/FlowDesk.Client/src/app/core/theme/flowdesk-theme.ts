import { definePreset } from '@primeuix/themes';
import Aura from '@primeuix/themes/aura';

const FlowDeskTheme = definePreset(Aura, {
  semantic: {
    primary: {
      50: '{indigo.50}',
      100: '{indigo.100}',
      200: '{indigo.200}',
      300: '{indigo.300}',
      400: '{indigo.400}',
      500: '{indigo.500}',
      600: '{indigo.600}',
      700: '{indigo.700}',
      800: '{indigo.800}',
      900: '{indigo.900}',
      950: '{indigo.950}'
    },

    colorScheme: {
      light: {
        surface: {
          0: '#ffffff',
          50: '#f9fafc',
          100: '#f2f4f8',
          200: '#e7eaf0',
          300: '#d9dee8',
          400: '#b8c0ce',
          500: '#8d98aa',
          600: '#667085',
          700: '#475467',
          800: '#344054',
          900: '#1d2939',
          950: '#101828'
        }
      }
    }
  },

  components: {
    card: {
      colorScheme: {
        light: {
          root: {
            background: '{surface.100}',
            borderRadius: '1.25rem',
            color: '{text.color}',
            shadow:
              '8px 8px 18px rgba(163, 171, 187, 0.25), -8px -8px 18px rgba(255, 255, 255, 0.9)'
          }
        }
      }
    }
  }
});

export default FlowDeskTheme;