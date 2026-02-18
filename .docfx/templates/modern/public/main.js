export default {
  start: () => {
    const script = document.createElement('script');
    script.src = 'https://context7.com/widget.js';
    script.setAttribute('data-library', '/codebeltnet/xunit');
    script.setAttribute('data-color', '#059669');
    script.setAttribute('data-position', 'bottom-right');
    script.setAttribute('data-placeholder', 'Ask about Codebelt.Extensions.Xunit...');
    script.async = true;
    document.body.appendChild(script);
  }
};
