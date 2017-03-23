;(function(window) {

  var svgSprite = '<svg>' +
    '' +
    '<symbol id="icon-book" viewBox="0 0 1024 1024">' +
    '' +
    '<path d="M742.4 921.6l-512 0c-14.1312 0-25.6-11.4688-25.6-25.6s11.4688-25.6 25.6-25.6l512 0c14.1312 0 25.6 11.4688 25.6 25.6s-11.4688 25.6-25.6 25.6z"  ></path>' +
    '' +
    '<path d="M844.8 153.6c-14.1312 0-25.6 11.4688-25.6 25.6l0 768c0 14.1312-11.4688 25.6-25.6 25.6l-563.2 0c-42.3424 0-76.8-34.4576-76.8-76.8s34.4576-76.8 76.8-76.8l460.8 0c42.3424 0 76.8-34.4576 76.8-76.8l0-614.4c0-42.3424-34.4576-76.8-76.8-76.8l-512 0c-42.3424 0-76.8 34.4576-76.8 76.8l0 768c0 70.5536 57.4464 128 128 128l563.2 0c42.3424 0 76.8-34.4576 76.8-76.8l0-768c0-14.1312-11.4688-25.6-25.6-25.6zM179.2 102.4l512 0c14.1312 0 25.6 11.4688 25.6 25.6l0 614.4c0 14.1312-11.4688 25.6-25.6 25.6l-460.8 0c-28.7744 0-55.3984 9.5744-76.8 25.6512l0-665.6512c0-14.1312 11.4688-25.6 25.6-25.6z"  ></path>' +
    '' +
    '</symbol>' +
    '' +
    '</svg>'
  var script = function() {
    var scripts = document.getElementsByTagName('script')
    return scripts[scripts.length - 1]
  }()
  var shouldInjectCss = script.getAttribute("data-injectcss")

  /**
   * document ready
   */
  var ready = function(fn) {
    if (document.addEventListener) {
      if (~["complete", "loaded", "interactive"].indexOf(document.readyState)) {
        setTimeout(fn, 0)
      } else {
        var loadFn = function() {
          document.removeEventListener("DOMContentLoaded", loadFn, false)
          fn()
        }
        document.addEventListener("DOMContentLoaded", loadFn, false)
      }
    } else if (document.attachEvent) {
      IEContentLoaded(window, fn)
    }

    function IEContentLoaded(w, fn) {
      var d = w.document,
        done = false,
        // only fire once
        init = function() {
          if (!done) {
            done = true
            fn()
          }
        }
        // polling for no errors
      var polling = function() {
        try {
          // throws errors until after ondocumentready
          d.documentElement.doScroll('left')
        } catch (e) {
          setTimeout(polling, 50)
          return
        }
        // no errors, fire

        init()
      };

      polling()
        // trying to always fire before onload
      d.onreadystatechange = function() {
        if (d.readyState == 'complete') {
          d.onreadystatechange = null
          init()
        }
      }
    }
  }

  /**
   * Insert el before target
   *
   * @param {Element} el
   * @param {Element} target
   */

  var before = function(el, target) {
    target.parentNode.insertBefore(el, target)
  }

  /**
   * Prepend el to target
   *
   * @param {Element} el
   * @param {Element} target
   */

  var prepend = function(el, target) {
    if (target.firstChild) {
      before(el, target.firstChild)
    } else {
      target.appendChild(el)
    }
  }

  function appendSvg() {
    var div, svg

    div = document.createElement('div')
    div.innerHTML = svgSprite
    svgSprite = null
    svg = div.getElementsByTagName('svg')[0]
    if (svg) {
      svg.setAttribute('aria-hidden', 'true')
      svg.style.position = 'absolute'
      svg.style.width = 0
      svg.style.height = 0
      svg.style.overflow = 'hidden'
      prepend(svg, document.body)
    }
  }

  if (shouldInjectCss && !window.__iconfont__svg__cssinject__) {
    window.__iconfont__svg__cssinject__ = true
    try {
      document.write("<style>.svgfont {display: inline-block;width: 1em;height: 1em;fill: currentColor;vertical-align: -0.1em;font-size:16px;}</style>");
    } catch (e) {
      console && console.log(e)
    }
  }

  ready(appendSvg)


})(window)